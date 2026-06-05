"use client";

import React, { useCallback, useEffect, useRef } from 'react';
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import useAuctionStore from "@/hooks/useAuctionStore";
import useBidStore from "@/hooks/useBidStore";
import { useParams } from "next/navigation";
import { User } from "next-auth";
import AuctionCreatedToast from "@/app/components/AuctionCreatedToast";
import toast from "react-hot-toast";
import { getDetailedViewData } from "@/app/actions/auctionActions";
import AuctionFinishedToast from "@/app/components/AuctionFinishedToast";

const baseUrl = process.env.NEXT_PUBLIC_BASE_URL;

type SignalRProviderProps = {
  user: User | null;
  children: React.ReactNode;
}

const SignalRProvider = (
  {
    user,
    children
  } : SignalRProviderProps
) => {
  const connection = useRef<HubConnection | null>(null);
  const setCurrentPrice = useAuctionStore(state => state.setCurrentPrice);
  const addBid = useBidStore(state => state.addBid);
  const params = useParams<{id:string}>();

  const handleAuctionCreated = useCallback((auction: Auction) => {
    if (user?.username !== auction.seller){
      return toast(<AuctionCreatedToast auction={auction} />, {
        duration: 10000,
      });
    }
  }, [user?.username]);

  const handleBidPlaced = useCallback((bid: Bid) => {
    if (bid.bidStatus.includes('Accepted')){
      setCurrentPrice(bid.auctionId, bid.amount);
    }

    if (params.id === bid.auctionId){
      addBid(bid)
    }
  }, [setCurrentPrice, addBid, params.id]);

  const handleAuctionFinished = useCallback((finishedAuction: AuctionFinished) => {
    const auction = getDetailedViewData(finishedAuction.auctionId);
    return toast.promise(auction, {
      loading: 'Loading',
      success: (auction: Auction) => <AuctionFinishedToast finishedAuction={finishedAuction} auction={auction} />,
      error: () => 'Auction finished'
    }, {success: { duration: 5000}, icon: null})
  }, []);

  useEffect(() => {
    if (!connection.current){
      connection.current = new HubConnectionBuilder()
        .withUrl(`${baseUrl}notifications`)
        .withAutomaticReconnect()
        .build();

      connection.current.start()
        .then(() => console.log("Connected to Hub connection..."))
        .catch(err => console.log(err));
    }

    connection.current.on("BidPlaced", handleBidPlaced);
    connection.current.on("AuctionCreated", handleAuctionCreated);
    connection.current.on("AuctionFinished", handleAuctionFinished);

    return () => {
      connection.current?.off("BidPlaced", handleBidPlaced);
      connection.current?.off("AuctionCreated", handleAuctionCreated);
      connection.current?.off("AuctionFinished", handleAuctionFinished);
    }
  },[handleBidPlaced, handleAuctionCreated, handleAuctionFinished]);

  return (
    children
  );
};

export default SignalRProvider;
"use client";

import React, { useEffect } from 'react';
import { User } from "next-auth";
import useBidStore from "@/hooks/useBidStore";
import { getBidsForAuction } from "@/app/actions/auctionActions";
import toast from "react-hot-toast";
import Heading from "@/app/components/Heading";
import BidItem from "@/app/auctions/details/[id]/BidItem";
import numberWithCommas from "@/app/lib/numberWithComma";
import EmptyFilter from "@/app/components/EmptyFilter";
import BidForm from "@/app/auctions/details/[id]/BidForm";

type BidListProps = {
  user: User | null;
  auction: Auction
}

const BidList = (
  {
    user,
    auction
  } : BidListProps
) => {
  const [loading, setLoading] = React.useState(true);
  const bids = useBidStore(state => state.bids);
  const setBids = useBidStore(state => state.setBids);
  const open = useBidStore(state => state.open);
  const setOpen = useBidStore(state => state.setOpen);
  const openForBids = new Date(auction.auctionEnd) > new Date();

  const highBid = bids.reduce((previous, current) => previous > current.amount
    ? previous
    : current.bidStatus.includes('Accepted')
      ? current.amount
      : previous, 0)

  useEffect(() => {
    getBidsForAuction(auction.id)
      .then((res: any) => {
        if (res.error){
          throw res.error
        }
        setBids(res as Bid[]);
      }).catch(error => {
        toast.error(error.message);
    }).finally(() => setLoading(false))
  }, [auction.id, setBids]);

  useEffect(() => {
    setOpen(openForBids);
  }, [openForBids, setOpen]);

  if (loading) {
    return <span>Loading bids...</span>;
  }

  return (
    <div className="rounded-lg shadow-md">
      <div className="py-2 px-4 bg-white">
        <div className="sticky top-0 bg-white p-2">
          <Heading title={`Current high bid is $${numberWithCommas(highBid)}`} />
        </div>
      </div>
      <div className="overflow-auto h-87.5 flex flex-col-reverse px-2">
        {
          bids.length === 0 ? (
            <EmptyFilter
              title="No bids for this item"
              subtitle="Please feel free to make a bid"
              />
          ) : (
            <>
              {
                bids.map((bid) => (
                  <BidItem bid={bid} key={bid.id} />
                ))
              }
            </>
          )
        }
      </div>
      <div className="px-2 pb-2 text-gray-500">
        { !open ? (
          <div className="flex items-center justify-center p-2 text-lg font-semibold">
            This auction has finished
          </div>
        ) : !user ? (
            <div className="flex items-center justify-center p-2 text-lg font-semibold">
              Please login to make a bid
            </div>
          ) : user && user.username === auction.seller ? (
            <div className="flex items-center justify-center p-2 text-lg font-semibold">
              You cannot bid on your own auction
            </div>
          ) : (
            <BidForm auctionId={auction.id} highBid={highBid} />
          )
        }
      </div>

    </div>
  );
};

export default BidList;
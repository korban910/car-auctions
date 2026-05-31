"use client";

import React, { useEffect } from 'react';
import { User } from "next-auth";
import useBidStore from "@/hooks/useBidStore";
import { getBidsForAuction } from "@/app/actions/auctionActions";
import toast from "react-hot-toast";
import Heading from "@/app/components/Heading";
import BidItem from "@/app/auctions/details/[id]/BidItem";

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

  if (loading) {
    return <span>Loading bids...</span>;
  }

  return (
    <div className="border-2 rounded-lg p-2 bg-gray-200">
      <Heading title="Bids" />
      {
        bids.map((bid) => (
          <BidItem bid={bid} key={bid.id} />
        ))
      }
    </div>
  );
};

export default BidList;
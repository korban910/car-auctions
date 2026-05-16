import React from 'react';
import CountdownTimer from "@/app/auctions/CountdownTimer";
import CarImage from "@/app/auctions/CarImage";
import Link from "next/link";

type AuctionCardProps = {
  auction: Auction;
}

const AuctionCard = (
  {
    auction
  } : AuctionCardProps
) => {
  return (
    <Link href={`/auctions/details/${auction.id}`}>
      <div className="relative w-full bg-gray-200 aspect-16/10 rounded-lg overflow-hidden">
        <CarImage imageUrl={auction.item.imageUrl} />
        <div className="absolute bottom-2 left-2">
          <CountdownTimer auctionEnd={auction.auctionEnd} />
        </div>
      </div>
      <div className="flex justify-between items-center mt-4">
        <h3 className="text-gray-700">{auction.item.make} {auction.item.model}</h3>
        <p className="font-semibold text-sm">{auction.item.year}</p>
      </div>
    </Link>
  );
};

export default AuctionCard;
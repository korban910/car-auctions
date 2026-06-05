import React from 'react';
import Link from "next/link";
import Image from "next/image";

type AuctionCreatedToastProps = {
  auction: Auction;
}

const AuctionCreatedToast = (
  {
    auction
  } : AuctionCreatedToastProps
) => {
  return (
    <Link
      href={`/auctions/details/${auction.id}`}
      className="flex flex-col items-center">
      <div className="flex flex-row items-center gap-2">
        <Image
          src={auction.item.imageUrl}
          alt='Image of car'
          height={80}
          width={80}
          className="rounded-lg w-auto h-auto"
          />
        <span>New Auction: {auction.item.make} {auction.item.model} has been added</span>
      </div>
    </Link>
  );
};

export default AuctionCreatedToast;
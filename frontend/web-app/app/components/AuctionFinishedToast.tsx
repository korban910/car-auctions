import React from 'react';
import Link from "next/link";
import Image from "next/image";
import numberWithComma from "@/app/lib/numberWithComma";

type AuctionFinishedToastProps = {
  finishedAuction: AuctionFinished;
  auction: Auction;
}

const AuctionFinishedToast = (
  {
    finishedAuction,
    auction
  } : AuctionFinishedToastProps
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
        <div className="flex flex-col">
          <span>Auction for {auction.item.make} {auction.item.model} has finished!</span>
          { finishedAuction?.itemSold && finishedAuction.amount ? (
            <p>Congrats to {finishedAuction.winner} who has won this auction for ${numberWithComma(finishedAuction?.amount)}</p>
          ): (
            <p>This item did not sell.</p>
          )}
        </div>
      </div>
    </Link>
  );
};

export default AuctionFinishedToast;
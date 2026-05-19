import React from 'react';
import { getDetailedViewData } from "@/app/actions/auctionActions";
import Heading from "@/app/components/Heading";
import CountdownTimer from "@/app/auctions/CountdownTimer";
import CarImage from "@/app/auctions/CarImage";
import DetailedSpecs from "@/app/auctions/details/[id]/DetailedSpecs";

type DetailsPageProps = {
  params: Promise<{
    id: string;
  }>
}

const DetailsAuction = async (
  {
    params
  } : DetailsPageProps
) => {
  const { id } = await params;
  const data = await getDetailedViewData(id);

  return (
    <>
      <div className="flex justify-between">
        <Heading title={`${data.item.make} ${data.item.model}`} />
        <div className="flex gap-3">
          <h3 className="text-2xl font-semibold">Time remaining:</h3>
          <CountdownTimer auctionEnd={data.auctionEnd} />
        </div>
      </div>
      <div className="grid grid-cols-2 gap-6 mt-3">
        <div className="relative w-full bg-gray-200 aspect-4/3 rounded-lg overflow-hidden">
          <CarImage imageUrl={data.item.imageUrl} />
        </div>
        <div className="border-2 rounded-lg p-2 bg-gray-200">
          <Heading title="Bids" />
        </div>
      </div>

      <div className="mt-3 grid grid-cols-1 rounded-lg">
        <DetailedSpecs auction={data} />
      </div>
    </>
  );
};

export default DetailsAuction;
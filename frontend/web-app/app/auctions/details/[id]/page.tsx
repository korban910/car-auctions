import React from 'react';
import { getDetailedViewData } from "@/app/actions/auctionActions";
import Heading from "@/app/components/Heading";
import CountdownTimer from "@/app/auctions/CountdownTimer";
import CarImage from "@/app/auctions/CarImage";
import DetailedSpecs from "@/app/auctions/details/[id]/DetailedSpecs";
import EditButton from "@/app/auctions/details/[id]/EditButton";
import { getCurrentUser } from "@/app/actions/authActions";
import DeleteButton from "@/app/auctions/details/[id]/DeleteButton";
import BidItem from "@/app/auctions/details/[id]/BidItem";
import BidList from "@/app/auctions/details/[id]/BidList";

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
  const user = await getCurrentUser();

  return (
    <>
      <div className="flex justify-between">
        <div className="flex items-center gap-3">
          <Heading title={`${data.item.make} ${data.item.model}`} />
          {user?.username === data.seller && (
            <>
              <EditButton id={data.id} />
              <DeleteButton id={data.id} />
            </>
          )}
        </div>
        <div className="flex gap-3">
          <h3 className="text-2xl font-semibold">Time remaining:</h3>
          <CountdownTimer auctionEnd={data.auctionEnd} />
        </div>
      </div>
      <div className="grid grid-cols-2 gap-6 mt-3">
        <div className="relative w-full bg-gray-200 aspect-16/10 rounded-lg overflow-hidden">
          <CarImage imageUrl={data.item.imageUrl} />
        </div>
        <BidList user={user} auction={data} />
      </div>

      <div className="mt-3 grid grid-cols-1 rounded-lg">
        <DetailedSpecs auction={data} />
      </div>
    </>
  );
};

export default DetailsAuction;
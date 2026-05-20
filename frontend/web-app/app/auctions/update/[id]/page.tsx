import React from 'react';
import { getDetailedViewData } from "@/app/actions/auctionActions";
import Heading from "@/app/components/Heading";
import AuctionForm from "@/app/auctions/AuctionForm";

type UpdateAuctionProps = {
  params: Promise<{
    id: string;
  }>
}

const UpdateAuction = async (
  {
    params
  } : UpdateAuctionProps
) => {
  const { id } = await params;
  const data = await getDetailedViewData(id);

  return (
    <div className="mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg">
      <Heading title="Update your auction" subtitle="Please update the details of your car
      (only these auction properties can be updated)" />
      <AuctionForm auction={data} key={data.id} />
    </div>
  );
};

export default UpdateAuction;
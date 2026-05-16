import React from 'react';

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
  return (
    <div>
      Update Auction {id}
    </div>
  );
};

export default UpdateAuction;
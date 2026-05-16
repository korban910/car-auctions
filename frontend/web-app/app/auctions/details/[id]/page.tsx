import React from 'react';

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
  return (
    <div>
      Details Page {id}
    </div>
  );
};

export default DetailsAuction;
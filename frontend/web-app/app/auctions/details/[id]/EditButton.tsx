import React from 'react';
import { Button } from "flowbite-react";
import Link from "next/link";

type EditButtonProps = {
  id: string;
}

const EditButton = (
  {
    id
  } : EditButtonProps
) => {
  return (
    <Button>
      <Link href={`/auctions/update/${id}`}>Update Auction</Link>
    </Button>
  );
};

export default EditButton;
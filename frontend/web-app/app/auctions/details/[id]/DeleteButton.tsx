"use client";

import React from 'react';
import { useRouter } from "next/navigation";
import { deleteAuction } from "@/app/actions/auctionActions";
import toast from "react-hot-toast";
import { Button, Spinner } from "flowbite-react";

type DeleteButtonProps = {
  id: string;
}

const DeleteButton = (
  {
    id
  } : DeleteButtonProps
) => {
  const [loading, setLoading] = React.useState(false);
  const router = useRouter();

  const handleDelete = () => {
    setLoading(true);
    deleteAuction(id)
      .then(res => {
        if (res.error){
          throw res.error;
        }
        router.push('/')
      })
      .catch(err => {
        toast.error(err.status + " " + err.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }

  return (
    <Button outline color='red' onClick={handleDelete}>
      { loading && <Spinner size="sm" className="mr-3" /> }
      Delete Auction
    </Button>
  );
};

export default DeleteButton;
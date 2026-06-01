"use client";

import React from 'react';
import { FieldValues, useForm } from "react-hook-form";
import useBidStore from "@/hooks/useBidStore";
import { placeBidForAuction } from "@/app/actions/auctionActions";
import numberWithCommas from "@/app/lib/numberWithComma";

type BidFormProps = {
  auctionId: string;
  highBid: number;
}

const BidForm = (
  {
    auctionId,
    highBid,
  } : BidFormProps
) => {
  const { register, handleSubmit, reset} = useForm();
  const addBid = useBidStore(state => state.addBid);

  const onSubmit = async (data: FieldValues) => {
    placeBidForAuction(auctionId, +data.amount)
      .then(bid => {
        addBid(bid);
        reset();
      });
  }

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="flex items-center border-2 rounded-lg py-2"
    >
      <input
        type="number"
        {...register("amount")}
        className="input-custom"
        placeholder={`Enter your bid (minimum bid is $${numberWithCommas(highBid + 1)}`}
      />
    </form>
  );
};

export default BidForm;
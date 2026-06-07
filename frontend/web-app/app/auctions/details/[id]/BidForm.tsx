"use client";

import React from 'react';
import { useForm } from "react-hook-form";
import useBidStore from "@/hooks/useBidStore";
import { placeBidForAuction } from "@/app/actions/auctionActions";
import numberWithCommas from "@/app/lib/numberWithComma";
import toast from "react-hot-toast";

type BidFormProps = {
  auctionId: string;
  highBid: number;
}

type BidFormValues = {
  amount: string;
}

const BidForm = (
  {
    auctionId,
    highBid,
  } : BidFormProps
) => {
  const { register, handleSubmit, setValue} = useForm<BidFormValues>({
    defaultValues: { amount : ''},
    shouldUnregister: false
  });
  const addBid = useBidStore(state => state.addBid);

  const onSubmit = async (data: BidFormValues) => {
    if (+data.amount <= highBid) {
      setValue('amount', '', { shouldDirty: false, shouldTouch: false });
      return toast.error(`Bid must be at least \$${numberWithCommas(highBid + 1)}`);
    }
    placeBidForAuction(auctionId, +data.amount)
      .then(bid => {
        if (bid.error){
          throw bid.error;
        }
        addBid(bid);
      }).catch(error => toast.error(error.message))
      .finally(() => setValue('amount', '', { shouldDirty: false, shouldTouch: false }));
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
"use client";

import React, { useEffect } from 'react';
import { FieldValues, useForm } from "react-hook-form";
import { Button, Spinner } from "flowbite-react";
import { usePathname, useRouter } from "next/navigation";
import Input from "@/app/components/Input";
import DateInput from "@/app/components/DateInput";
import { createAuction, updateAuction } from "@/app/actions/auctionActions";
import toast from "react-hot-toast";

type AuctionFormProps = {
  auction?: Auction;
}

const AuctionForm = (
  {
    auction
  } : AuctionFormProps
) => {
  const {
    control,
    reset,
    handleSubmit,
    setFocus,
    formState: {isSubmitting, isValid, isDirty}
  } = useForm({
    mode: "onTouched",
  });
  const router = useRouter();
  const pathname = usePathname();
  const onSubmit = async (data: FieldValues) => {
    try {
      let id = '';
      let response;
      if (pathname === '/auctions/create') {
        response = await createAuction(data);
        id = response.id;
      } else {
        if (auction) {
          response = await updateAuction(auction.id, data);
          id = auction.id;
        }
      }
      if (response.error) {
        toast.error(`${response.error.status} ${response.error.message}`);
        return;
      }
      router.push(`/auctions/details/${id}`);
    } catch (error: any) {
      toast.error(`${error.status} ${error.message}`);
    }
  }

  const handleCancel = () => {
    router.push("/");
  }

  useEffect(() => {
    if (auction){
      const {item} = auction;
      const {make, model, color, mileage, year} = item;
      reset({make, model, color, mileage, year});
    }
    setFocus('make')
  }, [setFocus, auction, reset]);

  return (
    <form className="flex flex-col mt-3" onSubmit={handleSubmit(onSubmit)}>
      <Input name="make" label="Make" control={control} rules={{ required: 'Make is required'}} />
      <Input name="model" label="Model" control={control} rules={{ required: 'Model is required'}} />
      <Input name="color" label="Color" control={control} rules={{ required: 'Color is required'}} />

      <div className="grid grid-cols-2 gap-3">
        <Input name="year" label="Year" control={control} type="number" rules={{ required: 'Year is required'}} />
        <Input name="mileage" label="Mileage" control={control} type="number" rules={{ required: 'Mileage is required'}} />
      </div>

      {
        pathname === "/auctions/create" && (
          <>
            <Input
              name="imageUrl"
              label="Image URL"
              control={control}
              rules={{ required: 'Image URL is required'}}
            />

            <div className="grid grid-cols-2 gap-3">
              <Input
                name="reservePrice"
                label="Reserve Price(enter 0 if no reserve)"
                control={control}
                type="number"
                rules={{ required: 'Reserve Price is required'}}
              />
              {/*<Input name="auctionEnd" label="Auction end date/time" control={control} type="datetime-local" rules={{ required: 'Auction end date is required'}} />*/}
              <DateInput
                name="auctionEnd"
                label="Auction end date/time"
                control={control}
                showTimeSelect
                dateFormat="dd MMMM yyyy h:mm a"
                rules={{ required: 'Auction end date is required'}}
              />
            </div>
          </>
        )
      }

      <div className="flex justify-between">
        <Button outline color="gray" onClick={handleCancel}>
          Cancel
        </Button>
        <Button
          outline
          color="green"
          type="submit"
          disabled={!isValid || !isDirty}
        >
          {isSubmitting && <Spinner size="sm" />}
          Submit
        </Button>
      </div>
    </form>
  );
};

export default AuctionForm;
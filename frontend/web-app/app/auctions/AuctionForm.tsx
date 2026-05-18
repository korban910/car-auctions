"use client";

import React, { useEffect } from 'react';
import { FieldValues, useForm } from "react-hook-form";
import { Button, Spinner } from "flowbite-react";
import { useRouter } from "next/navigation";
import Input from "@/app/components/Input";
import DateInput from "@/app/components/DateInput";
import { createAuction } from "@/app/actions/auctionActions";

const AuctionForm = () => {
  const {
    control,
    handleSubmit,
    setFocus,
    formState: {isSubmitting, isValid, isDirty}
  } = useForm({
    mode: "onTouched",
  });
  const router = useRouter();

  const onSubmit = async (data: FieldValues) => {
    console.log(data);
    try {
      const response = await createAuction(data);
      if (response.error){
        throw new Error(response.error)
      }
      router.push(`/auctions/details/${response.id}`);
    } catch (error) {
      console.log(error);
    }
  }

  const handleCancel = () => {
    router.push("/");
  }

  useEffect(() => {
    setFocus('make')
  }, [setFocus]);

  return (
    <form className="flex flex-col mt-3" onSubmit={handleSubmit(onSubmit)}>
      <Input name="make" label="Make" control={control} rules={{ required: 'Make is required'}} />
      <Input name="model" label="Model" control={control} rules={{ required: 'Model is required'}} />
      <Input name="color" label="Color" control={control} rules={{ required: 'Color is required'}} />

      <div className="grid grid-cols-2 gap-3">
        <Input name="year" label="Year" control={control} type="number" rules={{ required: 'Year is required'}} />
        <Input name="mileage" label="Mileage" control={control} type="number" rules={{ required: 'Mileage is required'}} />
      </div>

      <Input name="imageUrl" label="Image URL" control={control} rules={{ required: 'Image URL is required'}} />

      <div className="grid grid-cols-2 gap-3">
        <Input name="reservePrice" label="Reserve Price(enter 0 if no reserve)" control={control} type="number" rules={{ required: 'Reserve Price is required'}} />
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
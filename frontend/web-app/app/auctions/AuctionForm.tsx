"use client";

import React, { useEffect } from 'react';
import { FieldValues, useForm } from "react-hook-form";
import { Button, Spinner } from "flowbite-react";
import { useRouter } from "next/navigation";
import Input from "@/app/components/Input";

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

  const onSubmit = (data: FieldValues) => {
    console.log(data);
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

      <div className="grid grid-cols-2 gap-3">
        <Input name="reservePrice" label="Reserve Price(enter 0 if no reserve)" control={control} type="number" rules={{ required: 'Reserve Price is required'}} />
        <Input name="auctionEnd" label="Auction end data/time" control={control} type="date" rules={{ required: 'Auction end date is required'}} />
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
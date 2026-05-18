"use server";

import fetchWrapper from "@/app/lib/fetchWrapper";
import { FieldValues } from "react-hook-form";

const getData = async (query: string): Promise<PagedResult<Auction>> => {
  return fetchWrapper.get(`search${query}`);
}

const updateAuctionTest = async (): Promise<{status: number, message: string}> => {
  const data = {
    item: {
      mileage: Math.floor(Math.random() * 1000) + 1
    },
  }

  return fetchWrapper.put('auctions/afbee524-5972-4075-8800-7d1f9d7b0a0c', data);
}

const createAuction = async (data: FieldValues) => {
  return fetchWrapper.post(`auctions`, data);
}

export {
  getData,
  updateAuctionTest,
  createAuction,
}
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

const getDetailedViewData = async (id: string) : Promise<Auction> => {
  return fetchWrapper.get(`auctions/${id}`);
}

const updateAuction = async (id: string, data: FieldValues) => {
  return fetchWrapper.put(`auctions/${id}`, data);
}

const deleteAuction = async (id: string) => {
  return fetchWrapper.remove(`auctions/${id}`);
}

const getBidsForAuction = async (id: string) : Promise<Bid[]> => {
  return fetchWrapper.get(`bids/${id}`);
}

const placeBidForAuction = async (auctionId: string, amount: number) => {
  return fetchWrapper.post(`bids`, {auctionId, amount});
}

export {
  getData,
  updateAuctionTest,
  createAuction,
  getDetailedViewData,
  updateAuction,
  deleteAuction,
  getBidsForAuction,
  placeBidForAuction
}
"use server";

import { auth } from "@/auth";

const getData = async (query: string): Promise<PagedResult<Auction>> => {
  const res = await fetch(`http://localhost:6001/search${query}`);
  console.log(res);

  if (!res.ok) {
    throw new Error('Failed to fetch data.');
  }

  return res.json();
}

const updateAuctionTest = async (): Promise<{status: number, message: string}> => {
  const data = {
    item: {
      mileage: Math.floor(Math.random() * 1000) + 1
    },
  }

  const session = await auth();

  const res = await fetch(`http://localhost:6001/auctions/afbee524-5972-4075-8800-7d1f9d7b0a0c`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${session?.accessToken}`,
    },
    body: JSON.stringify(data),
  })

  if (!res.ok) {
    return {
      status: res.status,
      message: res.statusText,
    }
  }

  return { status: res.status, message: res.statusText };
}

export {
  getData,
  updateAuctionTest,
}
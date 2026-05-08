"use client";

import React, { useEffect } from 'react';
import AuctionCard from "@/app/auctions/AuctionCard";
import AppPagination from "@/app/components/AppPagination";
import { getData } from "@/app/actions/auctionActions";
import Filters from "@/app/auctions/Filters";
import { useParamStore } from "@/hooks/useParamsStore";
import { useShallow } from "zustand/react/shallow";
import qs from "query-string";

const Listings = () => {
  const [data, setData] = React.useState<PagedResult<Auction>>();
  // const pageNumber = useParamStore (state => state.pageNumber);
  const params = useParamStore(useShallow(state => ({
    pageNumber: state.pageNumber,
    pageSize: state.pageSize,
    searchTerm: state.searchTerm,
  })));
  const setParams = useParamStore(state => state.setParams);
  const url = qs.stringifyUrl({
    url: '',
    query: params
  }, {
    skipEmptyString: true
  })

  const setPageNumber = (pageNumber: number) => {
    setParams({pageNumber})
  }

  useEffect(()=> {
    getData(url)
      .then(data => {
        setData(data);
      })
  }, [url]);

  if (!data) {
    return <h1>Loading</h1>
  }

  return (
    <>
      <Filters />
      <div className="grid grid-cols-4 gap-6">
        {
          data && data.results.map((auction) => (
            <AuctionCard auction={auction} key={auction.id} />
          ))
        }
      </div>
      <div className="flex flex-row justify-center mt-4">
        <AppPagination
          currentPage={params.pageNumber}
          pageCount={data.pageCount}
          pageChanged={setPageNumber}
        />
      </div>
    </>
  );
};

export default Listings;
"use client";

import React, { useEffect, useState } from 'react';
import AuctionCard from "@/app/auctions/AuctionCard";
import AppPagination from "@/app/components/AppPagination";
import { getData } from "@/app/actions/auctionActions";
import Filters from "@/app/auctions/Filters";
import { useParamStore } from "@/hooks/useParamsStore";
import { useShallow } from "zustand/react/shallow";
import qs from "query-string";
import EmptyFilter from "@/app/components/EmptyFilter";
import useAuctionStore from "@/hooks/useAuctionStore";

const Listings = () => {
  // const [data, setData] = React.useState<PagedResult<Auction>>();
  // const pageNumber = useParamStore (state => state.pageNumber);
  const [loading, setLoading] = useState(true);
  const data = useAuctionStore(useShallow(state => ({
    auctions: state.auctions,
    totalCount: state.totalCount,
    pageCount: state.pageCount,
  })));

  const setData = useAuctionStore(state => state.setData);

  const params = useParamStore(useShallow(state => ({
    pageNumber: state.pageNumber,
    pageSize: state.pageSize,
    searchTerm: state.searchTerm,
    orderBy: state.orderBy,
    filterBy: state.filterBy,
    seller: state.seller,
    winner: state.winner,
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
        setLoading(false);
      })
  }, [url, setData]);

  if (loading) {
    return <h1>Loading</h1>
  }

  return (
    <>
      <Filters />
      {
        data.totalCount === 0 ? (
          <EmptyFilter showReset />
        ) : (
        <>
          <div className="grid grid-cols-4 gap-6">
            {
              data && data.auctions.map((auction) => (
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
        )
      }
    </>
  );
};

export default Listings;
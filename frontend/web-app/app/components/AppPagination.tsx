"use client";

import React from 'react';
import { Pagination } from "flowbite-react";

type PaginationProps = {
  currentPage: number;
  pageCount: number;
}

const AppPagination = (
  {
    currentPage,
    pageCount
  } : PaginationProps
) => {
  const [pageNumber, setPageNumber] = React.useState(currentPage);

  return (
      <Pagination
        currentPage={pageNumber}
        onPageChange={e => setPageNumber(e)}
        totalPages={pageCount}
        layout={"pagination"}
        showIcons
        className={'text-blue-500 mb-5'}
      />
  );
};

export default AppPagination;
"use client";

import React from 'react';
import { Pagination } from "flowbite-react";

type PaginationProps = {
  currentPage: number;
  pageCount: number;
  pageChanged: (page: number) => void;
}

const AppPagination = (
  {
    currentPage,
    pageCount,
    pageChanged,
  } : PaginationProps
) => {
  const handlePageChange = (page: number) => {
    pageChanged(page);
  }

  return (
      <Pagination
        currentPage={currentPage}
        onPageChange={e => handlePageChange(e)}
        totalPages={pageCount}
        layout={"pagination"}
        showIcons
        className={'text-blue-500 mb-5'}
      />
  );
};

export default AppPagination;
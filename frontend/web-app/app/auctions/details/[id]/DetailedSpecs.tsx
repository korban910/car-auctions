"use client";

import React from 'react';

import { Table, TableBody, TableCell, TableRow } from "flowbite-react";

type DetailedSpecsProps = {
  auction: Auction;
}

const DetailedSpecs = (
  {
    auction
  } : DetailedSpecsProps
) => {
  return (
    <Table striped={true}>
      <TableBody className="divide-y">
        <TableRow className="bg-white dark:border-gray-700 dark:bg-gray-800">
          <TableCell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
            Seller
          </TableCell>
          <TableCell>
            {auction.seller}
          </TableCell>
        </TableRow>
        <TableRow className="bg-white dark:border-gray-700 dark:bg-gray-800">
          <TableCell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
            Make
          </TableCell>
          <TableCell>
            {auction.item.make}
          </TableCell>
        </TableRow>
        <TableRow className="bg-white dark:border-gray-700 dark:bg-gray-800">
          <TableCell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
            Model
          </TableCell>
          <TableCell>
            {auction.item.model}
          </TableCell>
        </TableRow>
        <TableRow className="bg-white dark:border-gray-700 dark:bg-gray-800">
          <TableCell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
            Year manufactured
          </TableCell>
          <TableCell>
            {auction.item.year}
          </TableCell>
        </TableRow>
        <TableRow className="bg-white dark:border-gray-700 dark:bg-gray-800">
          <TableCell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
            Mileage
          </TableCell>
          <TableCell>
            {auction.item.mileage}
          </TableCell>
        </TableRow>
        <TableRow className="bg-white dark:border-gray-700 dark:bg-gray-800">
          <TableCell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
            Has reserve price?
          </TableCell>
          <TableCell>
            {auction.reservePrice > 0 ? 'Yes' : 'No'}
          </TableCell>
        </TableRow>
      </TableBody>
    </Table>
  );
};

export default DetailedSpecs;
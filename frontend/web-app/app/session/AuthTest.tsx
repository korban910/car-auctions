"use client";

import React from 'react';
import { Button, Spinner } from "flowbite-react";
import { updateAuctionTest } from "@/app/actions/auctionActions";

const AuthTest = () => {
  const [loading, setLoading] = React.useState(false);
  const [result, setResult] = React.useState<{ status: number, message: string} | null>(null);

  const handleUpdate = () => {
    setLoading(true);
    setResult(null);
    updateAuctionTest()
      .then(res => setResult(res))
      .catch(err => console.log(err))
      .finally(() => setLoading(false));
  }

  return (
    <div className="flex items-center p-4">
      <Button outline onClick={handleUpdate}>
        {loading ?? <Spinner size="sm" className="me-3" light />}
        Test auth
      </Button>
      <div>
        {JSON.stringify(result, null, 2)}
      </div>
    </div>
  );
};

export default AuthTest;
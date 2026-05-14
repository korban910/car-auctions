"use client";

import React from 'react';
import { useParamStore } from "@/hooks/useParamsStore";
import Heading from "@/app/components/Heading";
import { Button } from "flowbite-react";
import { signIn } from "next-auth/react";

type EmptyFilterProps = {
  title?: string;
  subtitle?: string;
  showReset?: boolean;
  showLogin?: boolean;
  callbackUrl?: string;
}

const EmptyFilter = (
  {
    title = "No matches for this filter",
    subtitle = "Try changing the filter or search term",
    showReset,
    showLogin,
    callbackUrl,
  } : EmptyFilterProps
) => {
  const reset = useParamStore(state => state.reset);

  return (
    <div className="flex flex-col gap-2 items-center justify-center h-[40vh] shadow-lg">
      <Heading title={title} subtitle={subtitle} center />
      <div className="mt-4">
        {showReset && (
          <Button outline onClick={reset}>
            Remove filters
          </Button>
        )}
        {showLogin && (
          <Button outline onClick={() => signIn(process.env.NEXT_PUBLIC_AUTH_DUENDE_IDENTITY_SERVER6_ID!, {redirectTo: callbackUrl})}>
            Login
          </Button>
        )}
      </div>
    </div>
  );
};

export default EmptyFilter;
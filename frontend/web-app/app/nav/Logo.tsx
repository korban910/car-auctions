"use client";

import React from 'react';
import { AiOutlineCar } from "react-icons/ai";
import { useParamStore } from "@/hooks/useParamsStore";
import { usePathname, useRouter } from "next/navigation";

const Logo = () => {
  const router = useRouter();
  const pathname = usePathname();
  const reset = useParamStore(state => state.reset);

  const handleReset = async () => {
    if (pathname !== '/') {
      await router.push("/");
      reset();
    }
  }

  return (
    <div
      onClick={handleReset}
      className="flex items-center gap-2 text-3xl font-semibold text-red-500 cursor-pointer"
    >
      <AiOutlineCar size={34} />
      <div>Car Auctions</div>
    </div>
  );
};

export default Logo;
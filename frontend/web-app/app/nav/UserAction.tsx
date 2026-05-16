"use client";

import React from 'react';
import { Dropdown, DropdownDivider, DropdownItem } from "flowbite-react";
import { User } from "next-auth";
import { HiCog, HiUser } from "react-icons/hi2";
import { AiFillCar, AiFillTrophy, AiOutlineLogout } from "react-icons/ai";
import { signOut } from "next-auth/react";
import { useParamStore } from "@/hooks/useParamsStore";
import { usePathname, useRouter } from "next/navigation";

type UserActionProps = {
  user: User;
}

const UserAction = ({ user }: UserActionProps) => {
  const router = useRouter();
  const pathname = usePathname();
  const setParams = useParamStore(state => state.setParams);

  const setWinner = () => {
    setParams({ winner: user.username, seller: undefined });
    if (pathname !== '/') {
      router.push('/');
    }
  }

  const setSeller = () => {
    setParams({ winner: undefined, seller: user.username });
    if (pathname !== '/') {
      router.push('/');
    }
  }

  return (
    <Dropdown inline label={`Welcome ${user.name}`} className="cursor-pointer">
      <DropdownItem icon={HiUser} onClick={setSeller}>
        My Auctions
      </DropdownItem>
      <DropdownItem icon={AiFillTrophy} onClick={setWinner}>
        Auctions won
      </DropdownItem>
      <DropdownItem icon={AiFillCar} href="/auctions/create">
        Sell my car
      </DropdownItem>
      <DropdownItem icon={HiCog} href="/session">
        Session (dev only!)
      </DropdownItem>
      <DropdownDivider />
      <DropdownItem
        icon={AiOutlineLogout}
        onClick={() => signOut({ redirectTo: '/' })}
      >
        Sign out
      </DropdownItem>
    </Dropdown>
  );
};

export default UserAction;
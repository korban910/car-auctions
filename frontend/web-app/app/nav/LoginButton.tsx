"use client";

import React from 'react';
import { Button } from "flowbite-react";
import { signIn } from "next-auth/react";

const LoginButton = () => {
  return (
    <Button
      outline
      onClick={() =>
        signIn(
          process.env.NEXT_PUBLIC_AUTH_DUENDE_IDENTITY_SERVER6_ID!,
          {redirectTo: '/'},
          {prompt: 'login'}
        )}
    >
      Login
    </Button>
  );
};

export default LoginButton;
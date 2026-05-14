import React from 'react';
import EmptyFilter from "@/app/components/EmptyFilter";

type SignInProps = {
  searchParams: Promise<{callbackUrl?: string}>
}

const SignIn = async (
  {
    searchParams
  } : SignInProps
) => {

  const resolvedSearchParams = await searchParams;
  const callbackUrl = resolvedSearchParams.callbackUrl;

  return (
    <EmptyFilter
      title="You need to be logged in to do that"
      subtitle="Please click below to login"
      showLogin
      callbackUrl={callbackUrl}
      />
  );
};

export default SignIn;
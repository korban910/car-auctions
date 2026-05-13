"use server";

import { auth } from "@/auth";

const getCurrentUser = async () => {
  try {
    const session = await auth();
    if (!session) {
      return null;
    }

    return session.user;
  } catch (error){
    console.log(error);
    return null;
  }
}

export {
  getCurrentUser
}
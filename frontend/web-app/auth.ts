import NextAuth, { Profile } from "next-auth";
import { OIDCConfig } from "next-auth/providers";
import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6";

export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [
    DuendeIDS6Provider({
      id: process.env.NEXT_PUBLIC_AUTH_DUENDE_IDENTITY_SERVER6_ID!,
      clientId: process.env.AUTH_DUENDE_IDENTITY_SERVER6_CLIENT_ID!,
      clientSecret: process.env.AUTH_DUENDE_IDENTITY_SERVER6_SECRET!,
      issuer: process.env.AUTH_DUENDE_IDENTITY_SERVER6_ISSUER!,
      authorization: {
        params: {
          scope: process.env.AUTH_DUENDE_IDENTITY_SERVER6_SCOPE,
        },
        url:
          process.env.AUTH_DUENDE_IDENTITY_SERVER6_ISSUER! +
          "/connect/authorize",
      },
      token: {
        url: `${process.env.AUTH_DUENDE_IDENTITY_SERVER6_ISSUER_INTERNAL}/connect/token`,
      },
      userinfo: {
        url: `${process.env.AUTH_DUENDE_IDENTITY_SERVER6_ISSUER_INTERNAL}/connect/token`,
      },
      idToken: true,
    } as OIDCConfig<Omit<Profile, "username">>),
  ],
  pages: {
    signIn: "/api/auth/signin",
  },
  callbacks: {
    async redirect({ url, baseUrl }) {
      return url.startsWith(baseUrl) ? url : baseUrl;
    },
    async authorized({ auth }) {
      return !!auth;
    },
    async jwt({ token, profile, account }) {
      if (account && account.access_token) {
        token.accessToken = account.access_token;
      }

      if (profile) {
        // User is available during sign-in
        token.username = profile.username;
      }
      return token;
    },
    async session({ session, token }) {
      if (token) {
        session.user.username = token.username as string;
        session.accessToken = token.accessToken as string;
      }
      return session;
    },
  },
});

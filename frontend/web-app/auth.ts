import NextAuth, { Profile } from "next-auth"
import { OIDCConfig } from "next-auth/providers"
import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6"

export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [
    DuendeIDS6Provider({
      id: process.env.NEXT_PUBLIC_AUTH_DUENDE_IDENTITY_SERVER6_ID!,
      clientId: process.env.AUTH_DUENDE_IDENTITY_SERVER6_CLIENT_ID!,
      clientSecret: process.env.AUTH_DUENDE_IDENTITY_SERVER6_SECRET!,
      issuer: process.env.AUTH_DUENDE_IDENTITY_SERVER6_ISSUER!,
      authorization: {
        params: {
          scope: process.env.AUTH_DUENDE_IDENTITY_SERVER6_SCOPE
        }
      },
      idToken: true
    } as OIDCConfig<Omit<Profile, 'username'>>),
  ],
  callbacks: {
    async jwt({ token, profile }) {
      if (profile) { // User is available during sign-in
        token.username = profile.username;
      }
      return token
    },
    async session({ session, token }) {
      if (token){
        session.user.username = token.username as string;
      }
      return session
    },
  },
})
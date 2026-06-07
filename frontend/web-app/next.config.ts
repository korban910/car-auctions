import type { NextConfig } from "next";
import withFlowbiteReact from "flowbite-react/plugin/nextjs";

const nextConfig: NextConfig = {
  /* config options here */
  reactCompiler: true,
  // allowedDevOrigins: ['10.0.0.88'],
  logging: {
    fetches: {
      fullUrl: true
    }
  },
  images: {
    remotePatterns: [
      {
        protocol: 'https', hostname: 'cdn.pixabay.com'
      },
      {
        protocol: 'https', hostname: 'upload.wikimedia.org'
      }
    ]
  }
};

export default withFlowbiteReact(nextConfig);
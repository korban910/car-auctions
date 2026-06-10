FROM oven/bun:canary-alpine AS deps
WORKDIR /app

COPY frontend/web-app/package*.json ./
COPY frontend/web-app/bun.lock ./
RUN bun install --omit-dev

FROM oven/bun:canary-alpine AS builder
WORKDIR /app
ARG NEXT_PUBLIC_BASE_URL
ARG NEXT_PUBLIC_AUTH_DUENDE_IDENTITY_SERVER6_ID

COPY --from=deps /app/node_modules ./node_modules
COPY frontend/web-app ./

ENV NEXT_PUBLIC_BASE_URL=$NEXT_PUBLIC_BASE_URL
ENV NEXT_PUBLIC_AUTH_DUENDE_IDENTITY_SERVER6_ID=$NEXT_PUBLIC_AUTH_DUENDE_IDENTITY_SERVER6_ID
ENV NEXT_TELEMETRY_DISABLED=1
RUN bun run build

FROM oven/bun:canary-alpine AS runner
WORKDIR /app

ENV NODE_ENV=production
ENV NEXT_TELEMETRY_DISABLED=1

RUN addgroup --system --gid 1001 nodejs
RUN adduser --system --uid 1001 nextjs

COPY --from=builder /app/public ./public
COPY --from=builder /app/package.json ./package.json
COPY --from=builder /app/next.config.ts ./next.config.ts

COPY --from=builder --chown=nextjs:nodejs /app/.next/standalone ./
COPY --from=builder --chown=nextjs:nodejs /app/.next/static ./.next/static

USER nextjs

EXPOSE 3000

ENV PORT=3000

CMD ["bun", "server.js"]
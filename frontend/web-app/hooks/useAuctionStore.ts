import { create } from "zustand/react";

type State = {
  auctions: Auction[],
  totalCount: number,
  pageCount: number
}

type Auctions = {
  setData: (data: PagedResult<Auction>) => void;
  setCurrentPrice: (auctionId: string, amount: number) => void;
}

const initialState: State = {
  auctions: [],
  pageCount: 0,
  totalCount: 0,
}

const useAuctionStore = create<State & Auctions>(
  (set) => ({
    ...initialState,

    setData: (data: PagedResult<Auction>) => {
      set(() => ({
        auctions: data.results,
        pageCount: data.pageCount,
        totalCount: data.totalCount,
      }))
    },

    setCurrentPrice: (auctionId: string, amount: number) => {
      set((state) => ({
        auctions: state.auctions.map((auction: Auction) => auction.id === auctionId
          ? {...auction, currentHighBid: amount} : auction),
      }))
    },
  })
)

export default useAuctionStore;
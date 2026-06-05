type Auction = {
  id: string;
  reservePrice: number;
  seller: string;
  winner?: string;
  soldAmount?: number;
  currentHighBid?: number;
  createdAt: string;
  updatedAt: string;
  auctionEnd: string;
  status: string;
  item: Item;
}

type AuctionFinished = {
  itemSold: boolean;
  auctionId: string;
  winner?: string;
  seller: string;
  amount?: number;
}
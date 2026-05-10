import { create } from "zustand/react";

type State = {
  pageNumber: number;
  pageSize: number;
  pageCount: number;
  searchTerm: string;
  orderBy: string;
  filterBy: string;
}

type Actions = {
  setParams: (pageNumber: Partial<State>) => void;
  reset: () => void;
}

const initialState: State = {
  pageNumber: 1,
  pageSize: 12,
  pageCount: 1,
  searchTerm: "",
  orderBy: "make",
  filterBy: "live"
}

const useParamStore = create<State & Actions>(
  (set) => ({
    ...initialState,

    setParams: (newParams: Partial<State>) => {
      set((state) => {
        if (newParams.pageNumber) {
          return {
            ...state,
            pageNumber: newParams.pageNumber,
          }
        } else {
          return {
            ...state,
            ...newParams,
            pageNumber: 1,
          }
        }
      })
    },

    reset: () => {
      set(initialState);
    }
  })
)

export {
  useParamStore
}
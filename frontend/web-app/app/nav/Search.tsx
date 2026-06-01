"use client";

import React, { useEffect } from 'react';
import { FaSearch } from "react-icons/fa";
import { useParamStore } from "@/hooks/useParamsStore";

const Search = () => {
  const setParams = useParamStore(state => state.setParams);
  const searchTerm = useParamStore(state => state.searchTerm);
  const inputRef = React.useRef<HTMLInputElement>(null);

  const handleSearch = () => {
    setParams({ searchTerm: inputRef.current?.value });
  }

  useEffect(() => {
    if (searchTerm === ""){
      if (inputRef.current){
        inputRef.current.value = "";
      }
    }
  }, [searchTerm]);

  return (
    <div className="flex w-1/2 items-center border-2 border-gray-300 rounded-full py-2 shadow-sm">
      <input
        onKeyDown={(e) => {
          if (e.key === "Enter") {
            handleSearch();
          }
        }}
        ref={inputRef}
        suppressHydrationWarning
        type="text"
        placeholder="Search for cars by make, model or color"
        className="input-custom"
        />
      <button onClick={handleSearch}>
        <FaSearch
          size={34}
          className="bg-red-400 text-white rounded-full p-2 cursor-pointer mx-2"
        />
      </button>
    </div>
  );
};

export default Search;
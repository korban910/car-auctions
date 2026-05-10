import React from 'react';
import { Button, ButtonGroup } from "flowbite-react";
import { useParamStore } from "@/hooks/useParamsStore";
import { AiOutlineClockCircle, AiOutlineSortAscending } from "react-icons/ai";
import { BsFillStopCircleFill, BsStopwatchFill } from "react-icons/bs";
import { GiFinishLine, GiFlame } from "react-icons/gi";

const pageSizeButtons = [4, 8, 12];
const orderButtons = [
  {label: 'Alphabetical', icon: AiOutlineSortAscending, value: 'make'},
  {label: 'End Date', icon: AiOutlineClockCircle, value: 'endingSoon'},
  {label: 'Recently Added', icon: BsFillStopCircleFill, value: 'new'},
]
const filterButtons = [
  {label: 'Live Auctions', icon: GiFlame, value: 'live'},
  {label: 'Ending < 6 hours', icon: GiFinishLine, value: 'endingSoon'},
  {label: 'Completed', icon: BsStopwatchFill, value: 'finished'},
]

const Filters = () => {

  const pageSize = useParamStore(state => state.pageSize);
  const orderBy = useParamStore(state => state.orderBy);
  const filterBy = useParamStore(state => state.filterBy);
  const setParams = useParamStore(state => state.setParams);

  return (
    <div className="flex justify-between items-center mb-4">
      <div>
        <span className="uppercase text-sm text-gray-500 mr-2">
          Filter By
        </span>
        <ButtonGroup>
          {
            filterButtons.map(({label, icon: Icon, value}) => (
              <Button
                key={value}
                onClick={() => setParams({ filterBy: value })}
                color={`${filterBy === value ? 'red' : 'gray'}`}
                className="focus:ring-0"
              >
                <Icon className="mr-3 w-4 h-4" />
                {label}
              </Button>
            ))
          }
        </ButtonGroup>
      </div>
      <div>
        <span className="uppercase text-sm text-gray-500 mr-2">
          Order By
        </span>
        <ButtonGroup>
          {
            orderButtons.map(({label, icon: Icon, value}) => (
              <Button
                key={value}
                onClick={() => setParams({ orderBy: value })}
                color={`${orderBy === value ? 'red' : 'gray'}`}
                className="focus:ring-0"
              >
                <Icon className="mr-3 w-4 h-4" />
                {label}
              </Button>
            ))
          }
        </ButtonGroup>
      </div>
      <div>
        <span className="uppercase text-sm text-gray-500 mr-2">
          Page Size
        </span>
        <ButtonGroup>
          {
            pageSizeButtons.map((value, index) => (
              <Button
                key={index}
                onClick={() => setParams({ pageSize: value })}
                color={`${pageSize === value ? 'red' : 'gray'}`}
                className="focus:ring-0"
              >
                {value}
              </Button>
            ))
          }
        </ButtonGroup>
      </div>
    </div>
  );
};

export default Filters;
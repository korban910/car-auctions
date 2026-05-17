import React from 'react';
import { useController, UseControllerProps } from "react-hook-form";
import { HelperText, Label, TextInput } from "flowbite-react";

type InputProps = {
  label: string;
  type?: string;
  showLabel?: boolean;
} & UseControllerProps

const Input = (props: InputProps) => {
  const {field, fieldState} = useController({...props});

  return (
    <div className="mb-3 block">
      {props.showLabel && (
        <div className="mb-2 block">
          <Label htmlFor={field.name}>{props.label}</Label>
        </div>
      )}
      <TextInput
        {...props}
        {...field}
        value={field.value || ''}
        type={props.type || "text"}
        placeholder={props.label}
        color={fieldState?.error ? 'failure' : !fieldState.isDirty ? '' : 'success'}
      />
      <HelperText color="failure">
        {fieldState?.error?.message}
      </HelperText>
    </div>
  );
};

export default Input;
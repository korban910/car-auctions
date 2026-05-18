import { auth } from "@/auth";

const baseUrl = process.env.NEXT_PUBLIC_BASE_URL;

const get = async (url: string) => {
  const requestOptions = {
    method: 'GET',
    headers: await getHeaders()
  };
  const response = await fetch(baseUrl + url, requestOptions);
  return handleResponse(response);
}

const put = async (url: string, body: unknown) => {
  const requestOptions = {
    method: 'PUT',
    headers: await getHeaders(),
    body: JSON.stringify(body)
  };
  const response = await fetch(baseUrl + url, requestOptions);
  return handleResponse(response);
}

const post = async (url: string, body: unknown) => {
  const requestOptions = {
    method: 'POST',
    headers: await getHeaders(),
    body: JSON.stringify(body)
  };
  const response = await fetch(baseUrl + url, requestOptions);
  return handleResponse(response);
}

const remove = async (url: string) => {
  const requestOptions = {
    method: 'DELETE',
    headers: await getHeaders()
  };
  const response = await fetch(baseUrl + url, requestOptions);
  return handleResponse(response);
}

const handleResponse = async (response: Response) => {
  const text = await response.text();
  const data = text && JSON.parse(text);

  if (response.ok){
    return data || response.statusText;
  } else {
    const error = {
      status: response.status,
      message: response.statusText,
    }
    return {error};
  }
}

const getHeaders = async (): Promise<Headers> => {
  const session = await auth();
  const headers = new Headers();
  headers.set('Content-Type', 'application/json');
  if (session){
    headers.set('Authorization', `Bearer ${session.accessToken}`);
  }
  return headers;
}

const fetchWrapper = {
  get,
  post,
  put,
  remove
}

export default fetchWrapper;
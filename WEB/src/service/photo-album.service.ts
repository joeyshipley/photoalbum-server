import axios from 'axios';
import { Album } from 'src/service/service.types';

type ServiceResponse<T> = {
  data: T,
  errors: [],
};

// TODO: real app would get this from ENV.
const BASE_URL = 'https://localhost:7038/api';
export default {
    albums: (): Promise<ServiceResponse<{ albums: Album[] }>> => {
      const url = `${ BASE_URL }/albums`;
      return handleResponse(axios.get(url));
    },
    albumPhotos: (albumId) => {
      const url = `${ BASE_URL }/photos/album/${ albumId }`;
      return handleResponse(axios.get(url));
    },
    photo: (photoId) => {
      const url = `${ BASE_URL }/photos/${ photoId }`;
      return handleResponse(axios.get(url));
    },
    like: (photoId) => {
      const url = `${ BASE_URL }/photos/${ photoId }/like`;
      return handleResponse(axios.post(url));
    },
}

function handleResponse(axiosCall) {
  return axiosCall
    .then((response) => response)
    .catch((error) => { return { errors: error.response?.data?.errors }});
}
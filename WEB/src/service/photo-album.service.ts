import axios from 'axios';
import {
  Album,
  AlbumPhoto,
  PhotoDetails,
  LikeResult
} from 'src/service/service.types';

type ServiceResponse<T> = {
  data: T,
  errors: [],
};

const BASE_URL = import.meta.env.VITE_API_URL;
export default {
    albums: (): Promise<ServiceResponse<{ albums: Album[] }>> => {
      const url = `${ BASE_URL }/albums`;
      return handleResponse(axios.get(url));
    },
    albumPhotos: (albumId): Promise<ServiceResponse<{ photos: AlbumPhoto[] }>> => {
      const url = `${ BASE_URL }/photos/album/${ albumId }`;
      return handleResponse(axios.get(url));
    },
    photo: (photoId): Promise<ServiceResponse<{ photo: PhotoDetails }>> => {
      const url = `${ BASE_URL }/photos/${ photoId }`;
      return handleResponse(axios.get(url));
    },
    likeIt: (photoId): Promise<ServiceResponse<{ photoLikeDetails: LikeResult }>> => {
      const url = `${ BASE_URL }/photos/${ photoId }/like`;
      return handleResponse(axios.post(url));
    },
}

function handleResponse(axiosCall) {
  return axiosCall
    .then((response) => response)
    .catch((error) => { return { errors: error.response?.data?.errors }});
}
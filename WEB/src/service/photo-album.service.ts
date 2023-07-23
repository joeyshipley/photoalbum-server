import axios from 'axios';

// TODO: real app would get this from ENV.
const BASE_URL = 'https://localhost:7038/api';
export default {
    albums() {
      const url = `${ BASE_URL }/albums`;
      return axios
        .get(url)
        .then((response) => response.data);
    },
    albumPhotos(albumId) {
      const url = `${ BASE_URL }/photos/album/${ albumId }`;
      return axios
        .get(url)
        .then((response) => response.data);
    },
    photo(photoId) {
      const url = `${ BASE_URL }/photos/${ photoId }`;
      return axios
        .get(url)
        .then((response) => response.data);
    },
    like(photoId) {
      const url = `${ BASE_URL }/photos/${ photoId }/like`;
      return axios
        .post(url)
        .then((response) => response.data);
    },
}
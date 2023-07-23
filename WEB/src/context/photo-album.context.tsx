import React, { createContext, useContext } from 'react';
import { useQuery } from 'react-query';
import queryOptions from 'src/util/react-query.options';
import photoAlbumService from 'src/service/photo-album.service';

export const defaultContextData = () => {
  return {
    errors: [],
    albums: [],
  };
};

export const PhotoAlbumContext = createContext(defaultContextData());

export const PhotoAlbumProvider = ({ children }) => {
  const albumsQuery = useQuery([ 'albums', 'errors' ], photoAlbumService.albums, queryOptions);
  const { albums } = albumsQuery.data ?? {};
  const { errors } = albumsQuery.error?.response?.data ?? [];

  return (
    <PhotoAlbumContext.Provider
      value={{ errors: errors ?? [], albums: albums ?? [] }}
    >
      {children}
    </PhotoAlbumContext.Provider>
  );
};

export const usePhotoAlbumContext = () => useContext(PhotoAlbumContext);
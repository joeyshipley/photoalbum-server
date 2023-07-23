/* eslint-disable @typescript-eslint/ban-types */
import React, { createContext, useContext, useState, useEffect } from 'react';
import photoAlbumService from 'src/service/photo-album.service';
import { Album,AlbumPhoto } from 'src/service/service.types';

type ContextSettings = {
    errors: [],
    albums: Album[],
    selectedAlbum: object | undefined,
    albumPhotos: AlbumPhoto[],
    selectedPhoto: object | undefined,

    chooseAlbum: Function,
    choosePhoto: Function,
};
export const defaultContextSettings = (): ContextSettings => {
  return {
    errors: [],
    albums: [],
    selectedAlbum: undefined,
    albumPhotos: [],
    selectedPhoto: undefined,

    chooseAlbum: () => {},
    choosePhoto: () => {},
  };
};

export const PhotoAlbumContext = createContext(defaultContextSettings());

export const PhotoAlbumProvider = ({ children }) => {
  const [ errors, setErrors ] = useState<[]>([]);
  const [ albums, setAlbums ] = useState<Album[]>([]);
  const [ albumPhotos, setAlbumPhotos ] = useState<AlbumPhoto[]>([]);
  const [ selectedAlbum, setSelectedAlbum ] = useState<object | undefined>(undefined);

  useEffect(() => {
    photoAlbumService.albums()
      .then((response) => {
        setAlbums(response.data?.albums ?? []);
        setErrors(response.errors ?? []);
      });
  }, []);

  const chooseAlbum = async (albumId) => {
    const id = Number(albumId);
    const album = albums.find((a: Album) => a.id === id);
    setSelectedAlbum(album);

    photoAlbumService.albumPhotos(albumId)
      .then((response) => {
        setAlbumPhotos(response.data?.photos ?? []);
        setErrors(response.errors ?? []);
      });
  };

  const contextData = {
    ...defaultContextSettings(),
    errors: errors,
    albums: albums,
    selectedAlbum,
    albumPhotos,

    chooseAlbum,
  };
  return (
    <PhotoAlbumContext.Provider
      value={contextData}
    >
      {children}
    </PhotoAlbumContext.Provider>
  );
};

export const usePhotoAlbumContext = () => useContext(PhotoAlbumContext);

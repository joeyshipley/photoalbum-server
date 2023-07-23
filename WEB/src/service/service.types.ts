export type Album = {
  id: number,
  title: string
};

export type AlbumPhoto = {
  id: number,
  albumId: number,
  title: string,
  thumbnailUrl: string,
}

export type PhotoDetails = {
  id: number,
  albumId: number,
  title: string,
  url: string,
  thumbnailUrl: string,
  likes: number | undefined,
};
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
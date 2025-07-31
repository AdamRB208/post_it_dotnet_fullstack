<script setup>
import { Album } from '@/models/Album.js';


defineProps({
  album: { type: Album, required: true }
})
</script>


<template>
  <RouterLink :to="{ name: 'Albums Page', params: { albumId: album.id } }" :title="`Go to the ${album.title}'s page!`">
    <div class="rounded shadow d-flex align-items-end text-light album-card mb-2 mt-2"
      :style="{ backgroundImage: `url(${album.coverImg})` }">
      <div class="d-flex justify-content-between align-items-center rounded p-2 flex-grow-1 title-section m-2">
        <div>
          <p>{{ album.title }}</p>
        </div>
        <div>
          <img :src="album.creator.picture" :alt="'A picture of ' + album.creator.name" class="rounded-5 me-2">
          <span>{{ album.creator.name }}</span>
        </div>
      </div>
    </div>
  </RouterLink>
</template>


<style lang="scss" scoped>
.album-card {
  min-height: 30dvh;
  background-size: cover;
  background-position: center;
}

.title-section {
  background-color: rgb(33 37 41 / 80%)
}
img {
  height: 2rem;
}

a {
  text-decoration: none;
}

</style>
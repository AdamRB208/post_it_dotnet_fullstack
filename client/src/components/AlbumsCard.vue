<script setup>
import { Album } from '@/models/Album.js';


defineProps({
  album: { type: Album, required: true }
})
</script>


<template>
  <!-- <RouterLink > -->
  <div class="rounded shadow d-flex align-items-end text-light album-card mb-2 mt-2" :style="{ backgroundImage: `url(${album.coverImg})` }">
    <div class="d-flex justify-content-between align-items-center rounded bg-dark p-2 flex-grow-1">
      <div>
        <p>{{ album.title }}</p>
      </div>
      <div>
        <img :src="album.creator.picture" :alt="'A picture of ' + album.creator.name" class="rounded-5 me-2">
        <span>{{ album.creator.name }}</span>
      </div>
    </div>
  </div>
  <!-- </RouterLink> -->
</template>


<style lang="scss" scoped>
.album-card {
  min-height: 30dvh;
  background-size: cover;
  background-position: center;
}

img {
  height: 2rem;
}

a {
  text-decoration: none;
}

</style>
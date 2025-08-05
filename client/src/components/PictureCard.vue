<script setup>
import { Picture } from '@/models/Picture.js';

const props = defineProps({
  picture: {type: Picture, required: true}
})


</script>


<template>
  <div class="position-relative">
    <img :src="picture.imgUrl" :alt="'Picture submitted by ' + picture.creator.name" class="w-100 rounded-4">
    <button class="btn btn-danger btn-sm rounded-circle position-absolute mt-1 me-1" title="Delete your picture"
      type="button"><span class="mdi mdi-close-circle-outline text-light"></span></button>
    <div class="position-absolute d-flex m-1">
      <img :src="picture.creator.picture" :alt="'A picture of ' + picture.creator.name" class="me-2 rounded-circle"
        height="30">
      <div>{{ picture.creator.name }}</div>
    </div>
  </div>
  <div class="d-flex justify-content-end">
    <button class="btn btn-vue rounded-pill mt-3 text-light " type="button">Add Picture</button>
  </div>
</template>


<style lang="scss" scoped>
button {
  top: 0;
  right: 0;
}

div.position-absolute {
  bottom: 0;
}
</style>
<script setup>
import { Picture } from '@/models/Picture.js';

const props = defineProps({
  picture: {type: Picture, required: true}
})


</script>


<template>
  <div class="position-relative">
    <img :src="picture.imgUrl" :alt="'Picture submitted by ' + picture.creator.name" class="w-100">
    <button class="btn btn-danger rounded-circle position-absolute" title="Delete your picture" type="button"><span class="mdi alpha-x-circle-outline"></span></button>
    <div class="position-absolute">
      <img :src="picture.creator.name" :alt="'A picture of ' + picture.creator.name" class="round-picture me-2" height="30">
      <div>{{ picture.creator.name }}</div>
    </div>
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
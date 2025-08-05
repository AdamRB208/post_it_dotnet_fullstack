<script setup>
import { AppState } from '@/AppState.js';
import { Picture } from '@/models/Picture.js';
import { picturesService } from '@/services/PicturesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';

const account = computed(() => AppState.account)

const props = defineProps({
  picture: {type: Picture, required: true}
})

async function deletePicture(pictureId) {
  const confirmed = await Pop.confirm('Are you sure you want to delete this picture?')
  if (!confirmed) return
  try {
    await picturesService.deletePicture(pictureId)
  }
  catch (error) {
    Pop.error(error, 'Could not delete Picture!');
    logger.error('COULD NOT DELETE PICTURE!', error)
  }
}

</script>


<template>
  <div class="position-relative">
    <img :src="picture.imgUrl" :alt="'Picture submitted by ' + picture.creator.name" class="w-100 rounded-4">
    <button v-if="account?.id == picture.creatorId" @click="deletePicture(picture.id)"
      class="btn btn-danger btn-sm rounded-circle position-absolute mt-1 me-1" title="Delete your picture"
      type="button"><span class="mdi mdi-close-circle-outline text-light"></span></button>
    <div class="position-absolute d-flex m-1">
      <img :src="picture.creator.picture" :alt="'A picture of ' + picture.creator.name" class="me-2 rounded-circle"
        height="30">
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
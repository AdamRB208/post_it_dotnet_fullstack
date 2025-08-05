<script setup>
import { picturesService } from '@/services/PicturesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()

const editablePictureData = ref({
  imgUrl: '',
  albumId: route.params.albumId
})

async function createPicture() {
  try {
    const picture = await picturesService.createPicture(editablePictureData.value)
    editablePictureData.value = {
      imgUrl: '',
      albumId: route.params.albumId
    }
    Modal.getOrCreateInstance('#pictureModal').hide()
  }
  catch (error) {
    Pop.error(error, 'Could not create Picture!');
    logger.error('COULD NOT CREATE PICTURE!', error)
  }
}

</script>


<template>
  <form @submit.prevent="createPicture()">
    <div class="mb-3 form-floating">
      <input v-model="editablePictureData.imgUrl" type="url" class="form-control" id="pictureImgUrl"
        placeholder="Picture URL...">
      <label for="pictureImgUrl" class="form-label">Picture URL</label>
    </div>
    <div class="text-end">
      <button class="btn btn-primary" type="submit">Submit</button>
    </div>
  </form>
</template>


<style lang="scss" scoped>

</style>
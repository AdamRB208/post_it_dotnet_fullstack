<script setup>
import { ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()

const editablePictureData = ref({
  imgUrl: '',
  albumId: route.params.albumId
})


</script>


<template>
  <form>
    <div class="mb-3 form-floating">
      <input v-model="editablePictureData.imgUrl" type="url" class="form-control" id="pictureImgUrl" placeholder="Picture URL...">
      <label for="pictureImgUrl" class="form-label">Picture URL</label>
    </div>
    <div class="text-end">
      <button class="btn btn-primary" type="submit">Submit</button>
    </div>
  </form>
</template>


<style lang="scss" scoped>

</style>
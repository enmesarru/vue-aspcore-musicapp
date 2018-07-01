<template>
  <div>
    <el-row id="test">
      <el-col :span="12">
        <el-form ref="trackForm" :model="track" label-width="120px" enctype="multipart/form-data">

          <el-form-item label="Track Name" prop="name">
            <el-input v-model="track.trackName"></el-input>
          </el-form-item>

          <el-form-item>
            <el-select v-model="track.albumId" placeholder="Select">
              <el-option
                v-for="item in albums"
                :key="item.id"
                :label="item.name"
                :value="item.id">
              </el-option>
            </el-select>
          </el-form-item>

          <el-form-item>
            <el-input-number v-model="track.trackNumber" :min="1" :max="255"></el-input-number>
          </el-form-item>

          <el-form-item>
            <input  ref="trackFile" @change="sendFileToData" type="file" :rules="[{ required: true, message: 'Album image is required'}]" />
          </el-form-item>

          <el-form-item>
            <el-button type="primary" @click="uploadFile()">Create</el-button>
            <el-button>Cancel</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
  </div>
</template>

<script>
    export default {
      name: 'track-edit',
      created () {
        this.getAlbums()
        this.getTrack()
      },
      methods: {
        getAlbums () {
          this.$http.get('http://localhost:5000/api/albums/')
            .then((response) => { this.albums = response.body }
              , (response) => { console.log(response.error) })
        },
        getTrack () {
          this.$http.get(`http://localhost:5000/api/tracks/${this.$route.params.id}`)
            .then((response) => { this.track = response.body }
            , response => { console.log(response) })
        },
        uploadFile () {

        },
        sendFileToData () {

        }
      },
      data () {
        return {
          albums: [],
          track: {
            name: '',
            trackNumber: '',
            albumId: null,
            trackFile: []
          }
        }
      }
    }
</script>

<style scoped>

</style>

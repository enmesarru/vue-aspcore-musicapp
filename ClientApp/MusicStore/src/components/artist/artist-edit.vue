<template>
  <div>
    <el-row>
      <el-col>
        <el-form ref="artist" :model="artist" label-width="120px">
          <el-form-item
            prop="name"
            :rules="[
              { required: true, message: 'Artist name is required'}
            ]"
            label="Artist name">
            <el-input v-model="artist.name"></el-input>
          </el-form-item>
          <el-form-item
            prop="shortBio"
            :rules="[
              { required: true, message: 'Biography is required'}
            ]"
            label="Biography">
            <el-input v-model="artist.shortBio" type="textarea"></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="onSubmit">Edit</el-button>
            <el-button @click="turnToPage">Back</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
  </div>
</template>

<script>
    export default {
      name: 'artist-name',
      data () {
        return {
          artist: {
            id: null,
            name: null,
            shortBio: null
          }
        }
      },
      created () {
        this.fetchSingleData()
      },
      methods: {
        onSubmit () {
          this.$http.put(`http://localhost:5000/api/artists/${this.genre.id}`, this.artist)
            .then((response) => {
              this.$message({
                type: 'success',
                message: 'Genre updated.'
              })
            }, response => { console.log(response.error) })
        },
        turnToPage () {
          this.$router.go(-1)
        },
        fetchSingleData () {
          this.$http.get(`http://localhost:5000/api/artists/${this.$route.params.id}`)
            .then((response) => { this.artist = response.body },
              response => { console.log(response) })
        }
      }
    }
</script>

<style scoped>

</style>
